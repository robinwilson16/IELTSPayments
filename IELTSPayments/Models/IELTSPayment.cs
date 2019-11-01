using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IELTSPayments.Models
{
    public class IELTSPayment
    {
        public int TransactionID { get; set; }

        [Display(Name = "Reference")]
        [StringLength(50)]
        public string TransactionRef { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Exam")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeExam { get; set; }

        [Display(Name = "Dup Certificate")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeDuplicateCertificate { get; set; }

        [Display(Name = "Mock Exam 1")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeMockExam1 { get; set; }

        [Display(Name = "Mock Exam 2")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeMockExam2 { get; set; }

        [Display(Name = "Transfer")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeTransfer { get; set; }

        [Display(Name = "Book")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeBooks { get; set; }

        [Display(Name = "Enquiry On Result")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeEnquiryOnResult { get; set; }

        [Display(Name = "Tips")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TipsClass { get; set; }

        [Display(Name = "Fees Recorded")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FeesRecorded { get; set; }

        [Display(Name = "Total Amount Paid")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; }

        [Display(Name = "Discrepancy")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Discrepancy { get; set; }
    }
}
