using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Mapping;
using Application.Services;
using AutoMapper;
using Contracts;
using Contracts.Interfaces;
using Contracts.Services;
using Domain.Entities;
using Domain.Exceptions.CategoryExceptions;
using Domain.Repositories;
using Infrastructure.Photos;
using Infrastructure.Security;
using Moq;
using NUnit.Framework;

namespace Application.UnitTests.Services
{
    [TestFixture]
    public class CategoryServiceTests
    {
         private IServiceManager _serviceManager;
         private readonly IMapper _mapper;
         private readonly Mock<IUnitOfWork> _unitOfWork;
         private readonly Mock<IUserAccessor> _userAccesor;
         private readonly Mock<IPhotoAccessor> _photoAccesor;
         
         public CategoryServiceTests()
         {
             {
                 var mappingConfig = new MapperConfiguration(mc =>
                 {
                     mc.AddProfile(new MappingProfiles());
                 });
                 IMapper mapper = mappingConfig.CreateMapper();
                 _mapper = mapper;
             }

             _unitOfWork = new Mock<IUnitOfWork>();
             _userAccesor = new Mock<IUserAccessor>();
             _photoAccesor = new Mock<IPhotoAccessor>();
             _serviceManager =
                 new ServiceManager(_unitOfWork.Object, _mapper, _userAccesor.Object, _photoAccesor.Object);
         }

         [Test]
         public async Task GetByIdAsync_WhenCalledWithExistingId_ReturnCategory()
         {
             _unitOfWork.Setup(u => u.CategoryRepository
                     .GetByIdAsync(Guid.Empty, CancellationToken.None))
                 .Returns(Task.FromResult(new Category{Id = Guid.Empty, Name = "Name"}));

             var category = await _serviceManager.CategoryService.GetByIdAsync(Guid.Empty, CancellationToken.None);
             
             Assert.That(category.Name, Is.EqualTo("Name"));

         }
         
         [Test]
         public async Task GetByIdAsync_WhenCalledWithNonExistingId_ThrowCategoryNotFoundException()
         {
             _unitOfWork.Setup(u => u.CategoryRepository
                     .GetByIdAsync(Guid.Empty, CancellationToken.None))
                 .Returns(Task.FromResult(new Category{Id = Guid.Empty, Name = "Name"}));
             

             Assert.ThrowsAsync<CategoryNotFoundException>( () =>
                 _serviceManager.CategoryService.GetByIdAsync(Guid.NewGuid(), CancellationToken.None));

         }

         [Test]
         public void CreateAsync_WhenCalledAndSuccessfull_CallCreate()
         {
             var category = new CategoryDto {Id = Guid.Empty, Name = "Test"};
             
             _serviceManager.CategoryService.CreateAsync(category);
             
             _unitOfWork.Verify(u => u.CategoryRepository.Create(It.IsAny<Category>()));
         }
         
         [Test]
         public  void  CreateAsync_WhenCalledAndFailed_ThrowCategoryCreateException()
         {
             var category = new CategoryDto {Id = Guid.Empty, Name = "Test"};
             
             _unitOfWork.Setup(u =>  u.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

             Assert.Throws<CategoryCreateException>(() => _serviceManager.CategoryService.CreateAsync(category));


         }
         
         [Test]
         public  void  DeleteAsync_WhenCalledAndFailed_ThrowCategoryDeleteException()
         {
             _unitOfWork.Setup(u => u.CategoryRepository
                     .GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                 .Returns(Task.FromResult(new Category{Id = Guid.Empty, Name = "Name"}));
             
             _unitOfWork.Setup(u =>  u.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(false));

             Assert.ThrowsAsync<CategoryDeleteException>(() => _serviceManager.CategoryService.DeleteAsync(Guid.Empty));


         }
         
         
         [Test]
         public void UpdateAsync_WhenCalledAndSuccessfull_CallUpdate()
         {
             var category = new CategoryDto {Id = Guid.Empty, Name = "Test"};
             
             _serviceManager.CategoryService.UpdateAsync(category);
             
             _unitOfWork.Verify(u => u.CategoryRepository.Update(It.IsAny<Category>()));
         }
         
         [Test]
         public  void  UpdateAsync_WhenCalledAndFailed_ThrowCategoryUpdateException()
         {
             var category = new CategoryDto {Id = Guid.Empty, Name = "Test"};
             
             _unitOfWork.Setup(u =>  u.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

             Assert.ThrowsAsync<CategoryUpdateException>(() => _serviceManager.CategoryService.UpdateAsync(category));


         }
    }
}