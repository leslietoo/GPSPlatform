using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808PackageImpl
{
    public abstract class JT808PackageBase<TBodies> : IJT808Package
        where TBodies: JT808Bodies
    {
        public JT808PackageBase(JT808Header jT808Header, ushort msgNum, TBodies bodies)
        {
            this.JT808Package = Create(jT808Header, msgNum,bodies);
        }

        public JT808Package JT808Package { get;}

        protected abstract JT808Package Create(JT808Header jT808Header, ushort msgNum, TBodies bodies);
    }
}
