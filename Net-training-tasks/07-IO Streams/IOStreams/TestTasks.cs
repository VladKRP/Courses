﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
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
            // TODO : Implement ReadPlanetInfoFromXlsx method using System.IO.Packaging + Linq-2-Xml
            // HINT : Please be as simple & clear as possible.
            //        No complex and common use cases, just this specified file.
            //        Required data are stored in Planets.xlsx archive in 2 files:
            //         /xl/sharedStrings.xml      - dictionary of all string values
            //         /xl/worksheets/sheet1.xml  - main worksheet
            const string mainPath = @"..\..\..\IOStreams.Tests\";
            string xlsxFilePath = mainPath + xlsxFileName;
            string sharedStringsPath = mainPath + @"xl\sharedStrings.xml";
            string worksheetPath = mainPath + @"xl\worksheets\sheet1.xml";
            List<PlanetInfo> planets = new List<PlanetInfo>();
            using (var package = Package.Open(xlsxFilePath, FileMode.Open, FileAccess.Read))
            {
                Uri uriSharedStrings = new Uri(sharedStringsPath);
                Uri uriWorksheet = new Uri(worksheetPath);
                PackagePart packagePartReplacement = package.CreatePart(uriWorksheet, "application/vnd.openxmlformats-officedocument.wordprocessingml.sheet1+xml");

            }
            
            throw new NotImplementedException();
		}


		/// <summary>
		/// Calculates hash of stream using specifued algorithm
		/// </summary>
		/// <param name="stream">source stream</param>
		/// <param name="hashAlgorithmName">hash algorithm ("MD5","SHA1","SHA256" and other supported by .NET)</param>
		/// <returns></returns>
		public static string CalculateHash(this Stream stream, string hashAlgorithmName)
		{
            // TODO : Implement CalculateHash method
            HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashAlgorithmName);
            if (hashAlgorithm == null)
                throw new ArgumentException();
            byte[] streamHashAsByteSequence = hashAlgorithm.ComputeHash(stream);
            string streamHashStringRepresentation = BitConverter.ToString(streamHashAsByteSequence).Replace("-","");
            return streamHashStringRepresentation;
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
            var fileText = File.ReadAllBytes(path);
            using (var decompressedStream = new MemoryStream())
            {
                using (var decompressionStream = new GZipStream(new MemoryStream(fileText), CompressionMode.Decompress))
                {
                    decompressionStream.CopyTo(decompressedStream);
                }
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
            // TODO : Implement ReadEncodedText method
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
