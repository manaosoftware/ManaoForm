using FormPackage6.Core.DomainModel.Form;
using FormPackage6.Core.Services.FormServices;
using FormPackage6.Core.Services.UmbracoServices;
using FormPackage6.Infrastructure.UmbracoServices;
using FormPackage6.Test.UnitTest.Base;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Umbraco.Tests.UnitTest.Form
{
    public class FormTest : UnitTestBase
    {
        private Mock<IMediaService> mediaService;
        public override void Initialize()
        {
            base.Initialize();

            IoCContainer.Configure(x => x.For<Umbraco.Core.Services.IContentService>().Use(new Mock<IContentService>().Object));

            var media = new Mock<IMedia>();
            mediaService = new Mock<IMediaService>();
            mediaService.Setup(x => x.CreateMedia(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), 0)).Returns(media.Object);
            IoCContainer.Configure(x => x.For<Umbraco.Core.Services.IMediaService>().Use(mediaService.Object));
            IoCContainer.Configure(x => x.For<UmbracoHelper>().Use(umbracoHelper));
        }

        [Test, Category("Form_Services_Validate")]
        public void Services_Validate_Email_OK()
        {
            FormPackage6.Core.DomainModel.Form.Form formModel = new FormPackage6.Core.DomainModel.Form.Form();

            Field field = new Field()
            {
                FieldType = new FieldType() { Type = FormPackage6.Core.Alias.PropertyAlias.Email },
                Value = "email@test.com"
            };

            formModel.Fields = new List<Field>();
            formModel.Fields.Add(field);

            IFormService formService = IoCContainer.GetInstance<IFormService>();

            var result = formService.Validate(formModel, null);

            Assert.AreEqual(result.Message, "success");
        }

        [Test, Category("Form_Services_Validate")]
        public void Services_Validate_Email_Failed()
        {
            FormPackage6.Core.DomainModel.Form.Form formModel = new FormPackage6.Core.DomainModel.Form.Form();

            Field field = new Field()
            {
                FieldType = new FieldType() { Type = FormPackage6.Core.Alias.PropertyAlias.Email },
                Name = "Email",
                Id = "0001",
                Value = "email.com"
            };

            formModel.Fields = new List<Field>();
            formModel.Fields.Add(field);

            IFormService formService = IoCContainer.GetInstance<IFormService>();
            var result = formService.Validate(formModel, null);

            Assert.IsNotNull(result.Errors.FirstOrDefault(x => x.FieldId == field.Id && x.ErrorType == "format"));
        }

        [Test, Category("Form_Services_Validate")]
        public void Services_Validate_Required_OK()
        {
            FormPackage6.Core.DomainModel.Form.Form formModel = new FormPackage6.Core.DomainModel.Form.Form();

            Field field = new Field()
            {
                FieldType = new FieldType() { Type = "TextBox" },
                Name = "Text",
                Id = "0001",
                Value = "Something",
                Mandatory = true
            };

            formModel.Fields = new List<Field>();
            formModel.Fields.Add(field);
            IFormService formService = IoCContainer.GetInstance<IFormService>();

            var result = formService.Validate(formModel, null); 

            Assert.AreEqual(result.Message, "success");
        }

        [Test, Category("Form_Services_Validate")]
        public void Services_Validate_Required_Failed()
        {
            FormPackage6.Core.DomainModel.Form.Form formModel = new FormPackage6.Core.DomainModel.Form.Form();

            Field field = new Field()
            {
                FieldType = new FieldType() { Type = "TextBox" },
                Name = "Text",
                Id = "0001",
                Value = String.Empty,
                Mandatory = true
            };

            formModel.Fields = new List<Field>();
            formModel.Fields.Add(field);

            IFormService formService = IoCContainer.GetInstance<IFormService>();

            var result = formService.Validate(formModel, null);

            Assert.IsNotNull(result.Errors.FirstOrDefault(x => x.FieldId == field.Id && x.ErrorType == "require"));
        }

        [Test, Category("Form_Services_UploadFiles")]
        public void Services_UploadFiles_OK()
        {
            FormPackage6.Core.DomainModel.Form.Form formModel = new FormPackage6.Core.DomainModel.Form.Form();

            Field field = new Field()
            {
                FieldType = new FieldType() { Type = FormPackage6.Core.Alias.PropertyAlias.Upload },
                Name = "Text",
                Id = "0001",
                Value = String.Empty,
                Mandatory = true
            };

            formModel.Fields = new List<Field>();
            formModel.Fields.Add(field);

            List<FileViewModel> uploadedFiles = new List<FileViewModel>();

            var file = new Mock<HttpPostedFileBase>();
            file.Setup(x => x.FileName).Returns("test.jpg");
            file.Setup(x => x.ContentType).Returns("image");

            FileViewModel fileModel = new FileViewModel()
            {
                file = file.Object,
                name = field.Id
            };
            uploadedFiles.Add(fileModel);

            IFormService formService = IoCContainer.GetInstance<IFormService>();

            formService.SaveUploadFiles(ref formModel, uploadedFiles);

            mediaService.Verify(x => x.Save(It.IsAny<IMedia>(), 0, true), Times.Exactly(1));
        }

        [Test, Category("Form_Services_UploadFiles")]
        public void Services_UploadFiles_Failed()
        {
            FormPackage6.Core.DomainModel.Form.Form formModel = new FormPackage6.Core.DomainModel.Form.Form();

            Field field = new Field()
            {
                FieldType = new FieldType() { Type = FormPackage6.Core.Alias.PropertyAlias.Upload },
                Name = "Text",
                Id = "0001",
                Value = String.Empty,
                Mandatory = true
            };

            formModel.Fields = new List<Field>();
            formModel.Fields.Add(field);

            List<FileViewModel> uploadedFiles = new List<FileViewModel>();

            var file = new Mock<HttpPostedFileBase>();
            file.Setup(x => x.FileName).Returns("test.jpg");
            file.Setup(x => x.ContentType).Returns("PDF");

            FileViewModel fileModel = new FileViewModel()
            {
                file = file.Object,
                name = field.Id
            };
            uploadedFiles.Add(fileModel);

            IFormService formService = IoCContainer.GetInstance<IFormService>();
            formService.SaveUploadFiles(ref formModel, uploadedFiles);

            mediaService.Verify(x => x.Save(It.IsAny<IMedia>(), 0, true));
        }

        [Test, Category("FormPlugin_Services")]
        public void FormPlugin_Services_Get_Field_Empty()
        {
            var formPluginService = IoCContainer.GetInstance<IFormPluginService>();

            var result = formPluginService.GetFields(0, "");
            Assert.IsTrue(result.Count() == 0);
        }

        [Test, Category("Form_Integration_Services")]
        public void Form_Integration_Services_Get_Dictionary_Null()
        {
            IoCContainer.Configure(x => x.For<IUmbracoService>().Use(new Mock<IUmbracoService>().Object));

            var service = IoCContainer.GetInstance<IIntegrationService>();
            Message message = null;
            var result = service.GetKeyFromDictionary(message);
            Assert.IsNull(result);
        }

        [Test, Category("Form_Integration_Services")]
        public void Form_Integration_Services_Get_Dictionary_Empty()
        {
            IoCContainer.Configure(x => x.For<IUmbracoService>().Use(new Mock<IUmbracoService>().Object));

            var service = IoCContainer.GetInstance<IIntegrationService>();
            Message message = new Message();
            var result = service.GetKeyFromDictionary(message);
            Assert.IsTrue(string.IsNullOrEmpty(result.Description));
        }

        [Test, Category("Form_Integration_Services")]
        public void Form_Integration_Services_Get_Dictionary_Corret()
        {
            Message message = new Message();
            message.Description = "Description";
            message.Key = "Key";

            var umbService = new Mock<IUmbracoService>();
            umbService.Setup(x => x.GetDictionaryItem(message.Key, message.Description)).Returns(message.Description);
            IoCContainer.Configure(x => x.For<IUmbracoService>().Use(umbService.Object));

            var service = IoCContainer.GetInstance<IIntegrationService>();

            var result = service.GetKeyFromDictionary(message);
            Assert.IsTrue(result.Description.Equals(message.Description));
        }


    }
}
