using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Module = Autofac.Module;

namespace Business.DependencyInjection.Autofac
{
    public class AutofacBusinessModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<UserManager>().As<IUserService>();


            builder.RegisterType<QuestionnaireRepository>().As<IQuestionnaireRepository>();
            builder.RegisterType<QuestionnaireManager>().As<IQuestionnaireService>();

            builder.RegisterType<QuestionRepository>().As<IQuestionRepository>();
            builder.RegisterType<QuestionManager>().As<IQuestionService>(); 
            
            builder.RegisterType<MultiAnswerRepository>().As<IMultiAnswerRepository>();
            builder.RegisterType<OptionAnswerRepository>().As<IOptionAnswerRepository>();
            builder.RegisterType<OpenAnswerRepository>().As<IOpenAnswerRepository>();

             builder.RegisterType<MultiSelectionRepository>().As<IMultiSelectionRepository>();
            builder.RegisterType<OptionSelectionRepository>().As<IOptionSelectionRepository>();

            builder.RegisterType<TakenQuestionnaireRepository>().As<ITakenQuestionnaireRepository>();

            builder.RegisterType<AnswerManager>().As<IAnswerService>();
            builder.RegisterType<TakenQuestionnaireManager>().As<ITakenQuestionnaireService>();

        }
    }
}
