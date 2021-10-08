using System;
using System.Collections.Generic;

namespace MinimalApi.Models
{
    public partial class CertApplication
    {
        public int Id { get; set; }
        public string StudentName { get; set; } = null!;
        public string StudentId { get; set; } = null!;
        public int ProgramId { get; set; }
        public bool ChangeNubCampus { get; set; }
        public int? FromNubCampus { get; set; }
        public int? ToNubCampus { get; set; }
        public int StudentType { get; set; }
        public string PhoneNo { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int MajorSubject { get; set; }
        public bool RegisteredConv { get; set; }
        public int? ConvocationId { get; set; }
        public DateTime ApplyDate { get; set; }
        public string TrackId { get; set; } = null!;
        /// <summary>
        /// User Id who approved In Department section
        /// </summary>
        public int ApprovedByDept { get; set; }
        /// <summary>
        /// User Id who approved In Accounts section
        /// </summary>
        public int ApprovedByAcc { get; set; }
        /// <summary>
        /// User Id who approved In Library section
        /// </summary>
        public int ApprovedByLib { get; set; }
        /// <summary>
        /// User Id who approved In ACAD section
        /// </summary>
        public int ApprovedByAcad { get; set; }
        public int ApprovedByExam { get; set; }
        /// <summary>
        /// Approval Status ID For Dept
        /// </summary>
        public int? ApvStatusDept { get; set; }
        /// <summary>
        /// Approval StatusID For Accounts section
        /// </summary>
        public int? ApvStatusAcc { get; set; }
        /// <summary>
        /// Approval StatusID For library section
        /// </summary>
        public int? ApvStatusLib { get; set; }
        /// <summary>
        /// Approval StatusID For ACADsection
        /// </summary>
        public int? ApvStatusAcad { get; set; }
        public int? ApvStatusExam { get; set; }
        public DateTime? ApvDeptDate { get; set; }
        public DateTime? ApvAccDate { get; set; }
        public DateTime? ApvLibDate { get; set; }
        public DateTime? ApvAcaddate { get; set; }
        public DateTime? ApvExamDate { get; set; }
        public bool? IsDelivered { get; set; }
        public byte[]? ExtraOne { get; set; }
        public string? ExtraTwo { get; set; }
        public string? ExtraThree { get; set; }
        public string? ExtraFour { get; set; }
    }
}
