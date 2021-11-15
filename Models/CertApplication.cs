using System;
using System.Collections.Generic;

namespace MinimalApi.Models
{
    public partial class CertApplication
    {
        public int Id { get; set; }
        public string? StudentName { get; set; }
        public string? StudentId { get; set; }
        public int ProgramId { get; set; }
        public bool ChangeNubCampus { get; set; }
        public int? FromNubCampus { get; set; }
        public int? ToNubCampus { get; set; }
        public int StudentType { get; set; }
        public string? PhoneNo { get; set; }
        public string? Address { get; set; }
        public int MajorSubject { get; set; }
        public bool RegisteredConv { get; set; }
        public int? ConvocationId { get; set; }
        public DateTime ApplyDate { get; set; }
        public string? TrackId { get; set; }
        public int ApprovedByDept { get; set; }
        public int ApprovedByAcc { get; set; }
        public int ApprovedByLib { get; set; }
        public int ApprovedByAcad { get; set; }
        public int ApprovedByExam { get; set; }
        public int? ApvStatusDept { get; set; }
        public int? ApvStatusAcc { get; set; }
        public int? ApvStatusLib { get; set; }
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
