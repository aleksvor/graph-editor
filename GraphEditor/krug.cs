﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor
{
    public partial class krug : Component
    {
        public krug()
        {
            InitializeComponent();
        }

        public krug(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
