using System;
using YAPMT.Domain.Helpers;
using YAPMT.Framework.Dtos;

namespace YAPMT.Domain.Dtos
{
    public class AssignmentDto : IDto
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string User { get; set; }

        public DateTime DueDate { get; set; }

        public bool Completed { get; set; }

        public int ProjectId { get; set; }

        public bool IsLate
        {
            get
            {
                return this.DueDate.Day - DateTime.Now.Day < 0;
            }
        }

        public string RelativeTime
        {
            get
            {
                return RelativeTimeFormat.RelativizeTime(this.DueDate);
            }
        }
    }
}
