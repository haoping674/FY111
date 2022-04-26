﻿using System;
using System.Collections.Generic;

#nullable disable

namespace FY111.Models.FY111
{
    public partial class MetaverseLog
    {
        public int MetaverseId { get; set; }
        public int MemberId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public virtual Member Member { get; set; }
        public virtual Metaverse Metaverse { get; set; }
    }
}