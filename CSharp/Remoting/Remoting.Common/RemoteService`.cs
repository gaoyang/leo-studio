using System;

namespace Remoting.Common
{
    public abstract class RemoteService : MarshalByRefObject
    {
        public abstract void Exec(string str);
    }
}
