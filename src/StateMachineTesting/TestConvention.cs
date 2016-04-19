using System;
using System.Linq;
using System.Reflection;
using Fixie;
using Should;

namespace StateMachineTesting
{
    public class TestConvention : Convention
    {
        public TestConvention()
        {
            Classes.NameEndsWith("Test");

            ClassExecution.CreateInstancePerClass();

            Methods
                .Where(
                x => x.IsVoid() && 
                x.IsPublic &&
                x.Name != "Setup" &&
                x.Name != "TearDown");

            FixtureExecution
                .Wrap<FixtureSetupTearDown>();
        }
    }

    public class FixtureSetupTearDown : FixtureBehavior
    {
        public void Execute(Fixture context, Action next)
        {
            FixtureSetUp(context);
            next();
            FixtureTearDown(context);
        }

        protected void FixtureSetUp(Fixture fixture)
        {
            fixture.Instance.GetType().TryInvoke("Setup",fixture.Instance);            
        }

        protected void FixtureTearDown(Fixture fixture)
        {
            fixture.Instance.GetType().TryInvoke("TearDown", fixture.Instance);
        }
    }

    public static class BehaviorBuilderExtensions
    {
        public static void TryField(this Fixie.Fixture context, string fieldName, object fieldValue)
        {
            var lifecycleMethod = context.Class.Type.GetField(fieldName, BindingFlags.Public | BindingFlags.Instance);

            if (lifecycleMethod == null)
                return;

            try
            {
                lifecycleMethod.SetValue(context.Instance, fieldValue);
            }
            catch (TargetInvocationException exception)
            {
                throw new PreservedException(exception.InnerException);
            }
        }


        public static void TryInvoke(this Type type, string method, object instance)
        {
            var lifecycleMethod =
                type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .SingleOrDefault(x => x.HasSignature(typeof(void), method));

            if (lifecycleMethod == null)
                return;

            try
            {
                lifecycleMethod.Invoke(instance, null);
            }
            catch (TargetInvocationException exception)
            {
                throw new PreservedException(exception.InnerException);
            }
        }
    }
}