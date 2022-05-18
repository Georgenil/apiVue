using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiNux.Domain
{
    public class Document
    {
        public int Id { get; set; }

        [MaxLength(128)]
        public string FileName { get; set; }
        public string? FilePath { get; set; }

        [MaxLength(10)]
        public string FileType { get; set; }

        [NotMapped]
        public string File { get; set; }

        public int Size { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
