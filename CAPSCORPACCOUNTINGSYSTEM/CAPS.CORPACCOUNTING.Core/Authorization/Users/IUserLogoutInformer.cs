using System.Collections.Generic;
using Abp.Dependency;
using Abp.RealTime;

namespace CAPS.CORPACCOUNTING.Authorization.Users
{
    public interface IUserLogoutInformer
    {
        void InformClients(IReadOnlyList<IOnlineClient> clients);
    }
}
