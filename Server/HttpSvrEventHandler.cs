﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swen1.MTCG_Petrovic.Server
 {
    /// <summary>This delegate is used for <see cref="HttpSvr"/> events.</summary>
    /// <param name="sender">Sending object.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void HttpSvrEventHandler(object sender, HttpSvrEventArgs e);
 }


