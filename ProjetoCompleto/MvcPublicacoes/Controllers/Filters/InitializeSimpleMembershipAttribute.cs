using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using MvcPublicacoes.Models;
using System.Web.Security;
using System.Linq;
using MvcPublicacoes.Persistencia;

namespace MvcPublicacoes.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UsersContext>(null);

                try
                {
                    using (var context = new UsersContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile",  "UserId", "UserName", autoCreateTables: true);

                    if (!Roles.RoleExists("Administrador")) {
                        Roles.CreateRole("Administrador");
                    }

                    if (!Roles.RoleExists("Pesquisador")) {
                        Roles.CreateRole("Pesquisador");
                    }

                    if (!WebSecurity.UserExists("Admin")) {
                        WebSecurity.CreateUserAndAccount("Admin","admin1234");

                        //Inclui o autor para o administrador
                        PubContext db = new PubContext();
                        Autor autor = new Autor();
                        autor.Nome = "Administrador do Sistema";
                        autor.UserId = WebSecurity.GetUserId("Admin");
                        db.Autores.Add(autor);
                        db.SaveChanges();
                    }

                    if (!Roles.GetRolesForUser("Admin").Contains("Administrador")) {
                        Roles.AddUsersToRoles(new[] { "Admin" }, new[] { "Administrador" });
                    }



                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}
