using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Contracts.Paging;
using Domain.Entities;
using Domain.Exceptions.CategoryExceptions;
using Domain.Exceptions.TopicExceptions;
using Domain.Exceptions.UserException;
using Moq;
using NUnit.Framework;

namespace Application.UnitTests.Services
{
    [TestFixture]
    public class TopicServiceTests : ServiceTestsBase
    {
        [Test]
        public async Task GetAllByCreatorUsernameAsync_WhenCalledWithExistingUsername_ReturnsTopics()
        {
            var user = new AppUser() {Id="Id", UserName = "username"};

            var topic = new Topic() {Title = "Title"};
            _userRepository.Setup(u =>
                    u.GetByUsernameAsync("username", It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(user));
            
            _topicRepository.Setup(t =>
                    t.GetAllByCreatorIdAsync("Id", It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new List<Topic>(){topic}.AsEnumerable()));

           var topics = await _serviceManager.TopicService.GetAllByCreatorUsernameAsync("username", CancellationToken.None);
           
           
           _topicRepository.Verify(t => t.GetAllByCreatorIdAsync("Id", It.IsAny<CancellationToken>()));
           Assert.That(topics, Is.Not.Null);
           Assert.That(topics.First().Title, Is.EqualTo(topic.Title));
        }

        [Test]
        public void GetAllByCreatorIdAsync_WhenCalledWithNonExistingUsername_ThrowsUserNotFound()
        {
            var user = new AppUser() {UserName="username", Id = "Id"};
            _userRepository.Setup(u =>
                    u.GetByUsernameAsync("username",  It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(user));
            
            Assert.ThrowsAsync<UserNotFound>(() =>_serviceManager.TopicService.GetAllByCreatorUsernameAsync("NonExistingId", CancellationToken.None));
        }

        [Test]
        public async Task GetAllByCategoryIdAsync_WhenCalledWithExistingCategoryId_ReturnsTopics()
        {
            var category = new Category() {Id = Guid.Empty, Name="Test"};
            var topic = new Topic() {Title = "Title"};
            _categoryRepository.Setup(c => c.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(category));
            _topicRepository.Setup(t => t.GetAllByCategoryIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new List<Topic>() {topic}.AsEnumerable()));

            var topics = await _serviceManager.TopicService.GetAllByCategoryIdAsync(Guid.Empty, new PagingParams(), CancellationToken.None);

            Assert.That(topics, Is.Not.Null);
            Assert.That(topics.First().Title, Is.EqualTo(topic.Title));

        }
        
        [Test]
        public void GetAllByCategoryIdAsync_WhenCalledWithNonExistingCategoryId_ThrowsCategoryNotFoundException()
        {
            var category = new Category() {Id = Guid.Empty, Name="Test"};
            var topic = new Topic() {Title = "Title"};
            _categoryRepository.Setup(c => c.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(category));
            _topicRepository.Setup(t => t.GetAllByCategoryIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new List<Topic>() {topic}.AsEnumerable()));

            Assert.ThrowsAsync<CategoryNotFoundException>(() => _serviceManager.TopicService.GetAllByCategoryIdAsync(Guid.NewGuid(), new PagingParams(), CancellationToken.None));
            
        }

        [Test]
        public async Task GetById_WhenCalledWithExistingId_ReturnsTopic()
        {
            var topic = new Topic() {Id = Guid.Empty, Title = "Title"};
            _topicRepository.Setup(t => t.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(topic));

            var result = await _serviceManager.TopicService.GetByIdAsync(Guid.Empty, CancellationToken.None);
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo(topic.Title));

        }
        
        [Test]
        public void GetById_WhenCalledWithNonExistingId_ThrowsTopicNotFoundException()
        {
            var topic = new Topic() {Id = Guid.Empty, Title = "Title"};
            _topicRepository.Setup(t => t.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(topic));

            Assert.ThrowsAsync<TopicNotFoundException>(() => _serviceManager.TopicService.GetByIdAsync(Guid.NewGuid(), CancellationToken.None));
        }

        [Test]
        public async Task GetAllAsync_WhenCalled_ReturnsTopic()
        {
            var topic = new Topic() {Id = Guid.Empty, Title = "Title"};
            _topicRepository.Setup(t => t.GetAllAsync( It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new List<Topic>(){topic}.AsEnumerable()));

            var topics = await _serviceManager.TopicService.GetAllAsync(new PagingParams());
            
            _topicRepository.Verify(t => t.GetAllAsync(It.IsAny<CancellationToken>()));
            Assert.That(topics, Is.Not.Null);
            Assert.That(topics.First().Title, Is.EqualTo(topic.Title));

        }

        [Test]
        public async Task CreateAsync_WhenCalled_CallCreateMethod()
        {
            var user = new AppUser() {Id = "", UserName = "Name"};
            var category = new Category() {Id = Guid.Empty, Name = "Category"};
            _userAccesor.Setup(u => u.GetUsername()).Returns("Name");
            _userRepository.Setup(u => u.GetByUsernameAsync("Name", It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(user));
            _categoryRepository.Setup(c => c.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(category));
            _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            await _serviceManager.TopicService.CreateAsync(new TopicDto()
            {
                Category = category
            });
            
            _topicRepository.Verify(t => t.Create(It.IsAny<Topic>()));

        }
        
        [Test]
        public void CreateAsync_WhenCalledWithNoAuthenticatedUser_ThrowUserNotFound()
        {
            var user = new AppUser() {Id = "", UserName = "Name"};
            var category = new Category() {Id = Guid.Empty, Name = "Category"};
            _userAccesor.Setup(u => u.GetUsername()).Returns("");
            _userRepository.Setup(u => u.GetByUsernameAsync("Name", It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(user));
            _categoryRepository.Setup(c => c.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(category));
            _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            Assert.ThrowsAsync<UserNotFound>(() => _serviceManager.TopicService.CreateAsync(new TopicDto()
            {
                Category = category
            }));
            
            

        }
        
        [Test]
        public void CreateAsync_WhenCalledWithNoExistingCategory_ThrowCategoryNotFoundException()
        {
            var user = new AppUser() {Id = "", UserName = "Name"};
            var category = new Category() {Id = Guid.Empty, Name = "Category"};
            _userAccesor.Setup(u => u.GetUsername()).Returns("Name");
            _userRepository.Setup(u => u.GetByUsernameAsync("Name", It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(user));
            _categoryRepository.Setup(c => c.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(category));
            _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            Assert.ThrowsAsync<CategoryNotFoundException>(() => _serviceManager.TopicService.CreateAsync(new TopicDto()
            {
                Category = new Category(){Id = Guid.NewGuid()}
            }));
            
            

        }
        [Test]
        public void CreateAsync_SaveChangesFailed_ThrowTopicCreateException()
        {
            var user = new AppUser() {Id = "", UserName = "Name"};
            var category = new Category() {Id = Guid.Empty, Name = "Category"};
            _userAccesor.Setup(u => u.GetUsername()).Returns("Name");
            _userRepository.Setup(u => u.GetByUsernameAsync("Name", It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(user));
            _categoryRepository.Setup(c => c.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(category));
            _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(false));

            Assert.ThrowsAsync<TopicCreateException>(() => _serviceManager.TopicService.CreateAsync(new TopicDto()
            {
                Category = category
            }));
            
           
        }

        [Test]
        public async Task DeleteAsync_WhenCalledWithExistingId_CalledDeleteAsync()
        {
            var topic = new Topic() {Id = Guid.Empty};
            _topicRepository.Setup(t => t.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(topic));
            _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            await _serviceManager.TopicService.DeleteAsync(topic.Id);
            
            _topicRepository.Verify(t => t.Delete(topic));

        }
        
        [Test]
        public void DeleteAsync_WhenCalledWithNoExistingId_ThrowsTopicNotFoundException()
        {
            var topic = new Topic() {Id = Guid.Empty};
            _topicRepository.Setup(t => t.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(topic));
            _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            Assert.ThrowsAsync<TopicNotFoundException>( () =>_serviceManager.TopicService.DeleteAsync(Guid.NewGuid()));
        }
        
        [Test]
        public void DeleteAsync_SaveChangesFailed_ThrowsTopicDeleteException()
        {
            var topic = new Topic() {Id = Guid.Empty};
            _topicRepository.Setup(t => t.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(topic));
            _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(false));

            Assert.ThrowsAsync<TopicDeleteException>( () =>_serviceManager.TopicService.DeleteAsync(topic.Id));
        }
        
         [Test]
        public void UpdateAsync_WhenCalledWithNonExistingTopicId_ThrowsTopicNotFoundException()
        {
            var category = new Category() {Id = Guid.Empty, Name = "Category"};
            _topicRepository.Setup(t => t.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new Topic() {Id = Guid.Empty, Title = "Title"}));
            _categoryRepository.Setup(c => c.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(category));
            _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            Assert.ThrowsAsync<TopicNotFoundException>(() => _serviceManager.TopicService.UpdateAsync(new TopicDto()
            {
                Id = Guid.NewGuid(),
                Title = "New Title",
                Category = category
            }));
        }
        
        [Test]
        public void UpdateAsync_WhenCalledWithNonExistingCategoryId_ThrowsCategoryNotFoundException()
        {
            var category = new Category() {Id = Guid.NewGuid(), Name = "Category"};
            _topicRepository.Setup(t => t.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new Topic() {Id = Guid.Empty, Title = "Title"}));
            _categoryRepository.Setup(c => c.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(category));
            _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            Assert.ThrowsAsync<CategoryNotFoundException>(() => _serviceManager.TopicService.UpdateAsync(new TopicDto()
            {
                Id = Guid.Empty,
                Title = "New Title",
                Category = category
            }));
        }
        
        [Test]
        public void UpdateAsync_Save_ThrowsCategoryNotFoundExceptionChangesFailed_ThrowsTopicUpdateException()
        {
            var category = new Category() {Id = Guid.Empty, Name = "Category"};
            _topicRepository.Setup(t => t.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new Topic() {Id = Guid.Empty, Title = "Title"}));
            _categoryRepository.Setup(c => c.GetByIdAsync(Guid.Empty, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(category));
            _unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(false));

            Assert.ThrowsAsync<TopicUpdateException>(() => _serviceManager.TopicService.UpdateAsync(new TopicDto()
            {
                Id = Guid.Empty,
                Title = "New Title",
                Category = category
            }));
        }
        
  
       
    }
}