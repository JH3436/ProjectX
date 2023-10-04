﻿namespace MVC_Project.Models
{
    public class MemberUseViewModel
    {
        public string? ActivityName { get; set; }
        public DateTime? VoteDate { get; set; }

        public string? GroupName { get; set; }
        public DateTime? EndDate { get; set; }
        public bool CanEdit { get; set; }

       public int LikeRecordID { get; set; } //為了要刪除
        
    };
}

