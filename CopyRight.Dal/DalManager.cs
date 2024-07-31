using CopyRight.Dal.Interfaces;
using CopyRight.Dal.Service;
using CopyRight.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace CopyRight.Dal
{
    public class DalManager
    {
        public IProject project { get; set; }
        public IRoleCode roles { get; set; }
        public IUser users { get; set; }
        public IPriorityCode priorities { get; set; }
        public IRealatedToCode realeted { get; set; }
        public IStatusCodeProject statusP { get; set; }
        public IStatusCodeUser statusU { get; set; }
        public ILead leads { get; set; }
        public ICustomer customers { get; set; }
        public ITasks tasks { get; set; }
        public IDocument document { get; set; }
        public ICommunication communications { get; set; }

        public DalManager()
        {
            ServiceCollection collections = new ServiceCollection();
            collections.AddSingleton<CopyRightContext>();
            collections.AddSingleton<IUser, UserService>();
            collections.AddSingleton<ILead, LeadService>();
            collections.AddSingleton<ICustomer, CustomerService>();
            collections.AddSingleton<ITasks, TasksService>();
            collections.AddSingleton<IProject, ProjectService>();
            collections.AddSingleton<IDocument, DocumentService>();
            collections.AddSingleton<ICommunication, CommunicationService>();
            collections.AddSingleton<IRealatedToCode, RelatedCodeService>();
            collections.AddSingleton<IPriorityCode, PriorityCodeService>();
            collections.AddSingleton<IRoleCode, RoleCodeService>();
            collections.AddSingleton<IStatusCodeProject, StatusCodeProjectService>();
            collections.AddSingleton<IStatusCodeUser, StatusCodeUserService>();

            var serviceprovider = collections.BuildServiceProvider();
            users = serviceprovider.GetRequiredService<IUser>();
            leads= serviceprovider.GetRequiredService<ILead>();
            customers = serviceprovider.GetRequiredService<ICustomer>();
            tasks = serviceprovider.GetRequiredService<ITasks>();
            project = serviceprovider.GetRequiredService<IProject>();
            document = serviceprovider.GetRequiredService<IDocument>();
            communications = serviceprovider.GetRequiredService<ICommunication>();

            roles = serviceprovider.GetRequiredService<IRoleCode>();
            priorities = serviceprovider.GetRequiredService<IPriorityCode>();
            statusP = serviceprovider.GetRequiredService<IStatusCodeProject>();
            statusU = serviceprovider.GetRequiredService<IStatusCodeUser>();
            realeted = serviceprovider.GetRequiredService<IRealatedToCode>();


        }
    }
}
