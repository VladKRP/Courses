using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace IOStreams
{

	public static class TestTasks
	{
		/// <summary>
		/// Parses Resourses\Planets.xlsx file and returns the planet data:
		///   Jupiter     69911.00
		///   Saturn      58232.00
		///   Uranus      25362.00
		///    ...
		/// See Resourses\Planets.xlsx for details
		/// </summary>
		/// <param name="xlsxFileName">source file name</param>
		/// <returns>sequence of PlanetInfo</returns>
		public static IEnumerable<PlanetInfo> ReadPlanetInfoFromXlsx(string xlsxFileName)
		{
            const string path = @"..\..\..\IOStreams.Tests\";
            string xlsxFilePath = path + xlsxFileName;
            const string workSheetPath = @"/xl/worksheets/sheet1.xml";
            const string stringValuesPath = @"/xl/sharedStrings.xml";

            using (var package = Package.Open(xlsxFilePath, FileMode.Open, FileAccess.Read))
            {
                Uri worksheetUri = new Uri(workSheetPath, UriKind.Relative);
                Uri stringValuesUri = new Uri(stringValuesPath, UriKind.Relative);
                PackagePart worksheetPart = package.GetPart(worksheetUri);
                PackagePart stringValuesPart = package.GetPart(stringValuesUri);

                XDocument xmlDocument = XDocument.Load(stringValuesPart.GetStream());
                var planetsName = from x in xmlDocument.Root.Elements()
                           from y in x.Elements()
                           select y.Value;
                planetsName = planetsName.Take((planetsName.Count() - 2));

                xmlDocument = XDocument.Load(worksheetPart.GetStream());
                var rootElements = from x in xmlDocument.Root.Elements()
                                    select x;
                var rowList = rootElements.Elements().Where(x => x.FirstAttribute.Name == "r").Skip(1);
                var planetsRadius = rowList.Elements().Where(x => x.FirstAttribute.Value.Contains("B")).Elements().Select(x => x.Value);
                for(int i = 0; i < planetsName.Count(); i++)
                {
                    PlanetInfo planet = new PlanetInfo();
                    planet.Name = planetsName.ElementAt(i);
                    planet.MeanRadius = double.Parse(planetsRadius.ElementAt(i));
                    yield return planet;
                }
            }
		}


		/// <summary>
		/// Calculates hash of stream using specifued algorithm
		/// </summary>
		/// <param name="stream">source stream</param>
		/// <param name="hashAlgorithmName">hash algorithm ("MD5","SHA1","SHA256" and other supported by .NET)</param>
		/// <returns></returns>
		public static string CalculateHash(this Stream stream, string hashAlgorithmName)
		{
            HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashAlgorithmName);
            if (hashAlgorithm == null)
                throw new ArgumentException();
            byte[] streamHashAsByteSequence = hashAlgorithm.ComputeHash(stream);
            return BitConverter.ToString(streamHashAsByteSequence).Replace("-", "");
        }


		/// <summary>
		/// Returns decompressed stream from file.
		/// </summary>
		/// <param name="fileName">source file</param>
		/// <param name="method">method used for compression (none, deflate, gzip)</param>
		/// <returns>output stream</returns>
		public static Stream DecompressStream(string fileName, DecompressionMethods method)
		{
            string path = @"..\..\..\IOStreams.Tests\" + fileName;
            var decompressedStream = new MemoryStream();
            using (var fileToDecompress = File.OpenRead(path))
            {
                    if (method == DecompressionMethods.None)
                        fileToDecompress.CopyTo(decompressedStream);
                    else if (method == DecompressionMethods.Deflate)
                    {
                        using (var decompressor = new DeflateStream(fileToDecompress, CompressionMode.Decompress))
                            decompressor.CopyTo(decompressedStream);
                    }
                    else if (method == DecompressionMethods.GZip)
                    {
                        using (var decompressor = new GZipStream(fileToDecompress, CompressionMode.Decompress))
                            decompressor.CopyTo(decompressedStream);
                    }
                    decompressedStream.Position = 0;
                    return decompressedStream;
            }
            
        }


		/// <summary>
		/// Reads file content econded with non Unicode encoding
		/// </summary>
		/// <param name="fileName">source file name</param>
		/// <param name="encoding">encoding name</param>
		/// <returns>Unicoded file content</returns>
		public static string ReadEncodedText(string fileName, string encoding)
		{
            string path = @"..\..\..\IOStreams.Tests\" + fileName;
            byte[] fileText = File.ReadAllBytes(path);
            var convertedFileText = Encoding.Convert(Encoding.GetEncoding(encoding), Encoding.Unicode, fileText);
            return Encoding.Unicode.GetString(convertedFileText);
		}
	}


	public class PlanetInfo : IEquatable<PlanetInfo>
	{
		public string Name { get; set; }
		public double MeanRadius { get; set; }

		public override string ToString()
		{
			return string.Format("{0} {1}", Name, MeanRadius);
		}

		public bool Equals(PlanetInfo other)
		{
			return Name.Equals(other.Name)
				&& MeanRadius.Equals(other.MeanRadius);
		}
	}



}
