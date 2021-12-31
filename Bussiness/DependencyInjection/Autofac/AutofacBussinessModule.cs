using Autofac;
using Bussiness.Abstract;
using Bussiness.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DependencyInjection.Autofac
{
    public class AutofacBussinessModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<UserManager>().As<IUserService>();


            builder.RegisterType<QuestionnaireRepository>().As<IQuestionnaireRepository>();
            builder.RegisterType<QuestionnaireManager>().As<IQuestionnaireService>();

        }
    }
}
