using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IELTSPayments.Models
{
    public class IELTSTransaction
    {
        public int PaymentID { get; set; }

        public int TransactionID { get; set; }

        [StringLength(255)]
        public string PaymentResponse { get; set; }

        [StringLength(50)]
        public string PaymentReference { get; set; }

        [Display(Name = "Reference")]
        [StringLength(53)]
        public string BritishCouncilRef { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Status")]
        [StringLength(255)]
        public string PaymentStatus { get; set; }

        public Guid? TransactionGUID { get; set; }

        [StringLength(50)]
        public string Forename { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(500)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeExam { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeDuplicateCertificate { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeMockExam1 { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeMockExam2 { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeTransfer { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeBooks { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TipsClass { get; set; }

        [Display(Name = "Total Amount")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeTotal { get; set; }

        [StringLength(50)]
        public string SystemUsed { get; set; }

        [StringLength(1)]
        public string ReportType { get; set; }

        public int RequiredEmails { get; set; }
        public int SentEmails { get; set; }

        public bool IsPaymentDiscrepancy { get; set; }

        public int EmailsRemaining { get; set; }
        public int EmailRequiredDuplicateCert { get; set; }
        public int EmailSentDuplicateCert { get; set; }
        public int EmailRequiredTransfer { get; set; }
        public int EmailSentTransfer { get; set; }
        public int EmailRequiredPractice { get; set; }
        public int EmailSentPractice { get; set; }
        public int EmailRequiredMock { get; set; }
        public int EmailSentMock { get; set; }
        public int EnquiryOnResult { get; set; }

        [Display(Name = "Progress")]
        [StringLength(10)]
        public string EmailProgress { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PaymentExpected { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PaymentMade { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PaymentDiscrepancy { get; set; }
    }
}
