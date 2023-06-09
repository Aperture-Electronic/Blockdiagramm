﻿using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace BlockdiagrammBackend.Models.Sources
{
    [Serializable]
    public class SourceFile
    {
        /// <summary>
        /// The hash of source file calculated by the SHA-256 hash algorithm
        /// </summary>
        public string Hash { get; }

        /// <summary>
        /// Path to the corresponding source file
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Type of the source file.
        /// This will determine how to interpret the source
        /// </summary>
        public SourceFileType Type { get; }

        /// <summary>
        /// The short name of the file (without the path)
        /// </summary>
        public string ShortName => Path.GetFileName(FilePath);

        /// <summary>
        /// Get the relative location (only path) of the file
        /// </summary>
        /// <param name="projectPath">Project path relative to</param>
        public string GetRelativeLocation(string projectPath) => Path.GetDirectoryName(GetRelativePath(projectPath)) ?? "";

        /// <summary>
        /// Get the relative path (with file name) of the file
        /// </summary>
        /// <param name="projectPath">Project path relative to</param>
        public string GetRelativePath(string projectPath) => Path.GetRelativePath(projectPath, FilePath);

        public SourceFile(string filePath) : this(filePath, Path.GetExtension(filePath) switch
        {
            "v" => SourceFileType.VerilogSource,
            "sv" => SourceFileType.SystemVerilogSource,
            "svh" => SourceFileType.SystemVerilogHeader,
            "vhd" => SourceFileType.VHDLSource,
            _ => SourceFileType.SystemVerilogSource // Default
        })
        { }

        public SourceFile(string filePath, SourceFileType type)
        {
            FilePath = filePath;
            Type = type;

            // Calculate the path hash
            byte[] hashValue = SHA256.HashData(Encoding.UTF8.GetBytes(filePath));
            Hash = Convert.ToBase64String(hashValue);
        }

        /// <summary>
        /// Get if the file existed
        /// </summary>
        public bool Exist => !string.IsNullOrEmpty(FilePath) && File.Exists(FilePath);

        /// <summary>
        /// Content of the file (this property will not be serialize)
        /// </summary>
        [JsonIgnore, IgnoreDataMember]
        public string Content { get; private set; } = string.Empty;

        /// <summary>
        /// Read and update the file content
        /// </summary>
        /// <exception cref="FileNotFoundException">The target file is not on the disk</exception>
        public async Task ReadFileContent()
        {
            if (!Exist)
            {
                throw new FileNotFoundException("The source file is not found", FilePath);
            }

            using FileStream fileStream = File.OpenRead(FilePath);
            using StreamReader streamReader = new(fileStream);
            Content = await streamReader.ReadToEndAsync();
            streamReader.Close();
        }

        [JsonIgnore, IgnoreDataMember]
        public string ContentHash
        {
            get
            {
                // Get hash of content by SHA-256
                byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(Content));
                return Convert.ToBase64String(hash);
            }
        }

        public override string ToString() => $"{FilePath}({Hash}) :: {Type}";
    }
}
