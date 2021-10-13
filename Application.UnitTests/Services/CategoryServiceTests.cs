using System;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Domain.Entities;
using Domain.Exceptions.CategoryExceptions;
using Moq;
using NUnit.Framework;

namespace Application.UnitTests.Services
{
    [TestFixture]
    public class CategoryServiceTests : ServiceTestsBase
    {
        
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
         public void GetByIdAsync_WhenCalledWithNonExistingId_ThrowCategoryNotFoundException()
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
             
             _unitOfWork.Setup(u =>  u.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(false));

             Assert.ThrowsAsync<CategoryCreateException>(() => _serviceManager.CategoryService.CreateAsync(category));


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
             
             _unitOfWork.Setup(u =>  u.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(false));

             Assert.ThrowsAsync<CategoryUpdateException>(() => _serviceManager.CategoryService.UpdateAsync(category));


         }
    }
}