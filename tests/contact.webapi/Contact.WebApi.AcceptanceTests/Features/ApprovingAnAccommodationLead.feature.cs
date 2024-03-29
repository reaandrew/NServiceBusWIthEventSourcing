﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.17929
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

#region Designer generated code

using TechTalk.SpecFlow;

#pragma warning disable

namespace Contact.WebApi.AcceptanceTests.Features
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("ApprovingAnAccommodationLead")]
    public partial class ApprovingAnAccommodationLeadFeature
    {
        private static TechTalk.SpecFlow.ITestRunner testRunner;

#line 1 "ApprovingAnAccommodationLead.feature"
#line hidden

        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            var featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"),
                                                                "ApprovingAnAccommodationLead",
                                                                "In order to increase the company revenue\nAs a Product Improvement user\nI want to " +
                                                                "be able to approve valid accommodation leads\nAnd increase the stock of hotels wh" +
                                                                "ich can be advertised", ProgrammingLanguage.CSharp,
                                                                ((string[]) (null)));
            testRunner.OnFeatureStart(featureInfo);
        }

        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }

        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }

        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }

        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }

        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Approve a valid AccommodationLead")]
        public virtual void ApproveAValidAccommodationLead()
        {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Approve a valid AccommodationLead",
                                                                  ((string[]) (null)));
#line 7
            this.ScenarioSetup(scenarioInfo);
#line hidden
            var table1 = new TechTalk.SpecFlow.Table(new string[]
                {
                    "Name",
                    "Email"
                });
            table1.AddRow(new string[]
                {
                    "Joe Bloggs",
                    "joe@test.com"
                });
#line 8
            testRunner.Given("an Accommodation Lead exists with the following information:", ((string) (null)), table1,
                             "Given ");
#line 11
            testRunner.When("I approve the Accommodation Lead", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)),
                            "When ");
#line 12
            testRunner.And("I query the system for the Accommodation Lead", ((string) (null)),
                           ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 13
            testRunner.Then("the Approval status of the Accommodation Lead should be true", ((string) (null)),
                            ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion