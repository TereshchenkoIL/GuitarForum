using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Contracts.Paging;
using Contracts.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   
    public class TopicsController : BaseApiController
    {

        public TopicsController(IServiceManager serviceManager) : base(serviceManager)
        {
            
        }
           

        [HttpGet("all")]
        public async Task<ActionResult<List<TopicDto>>> GetAllTopics([FromQuery] PagingParams param)
        {
            var topics = await ServiceManager.TopicService.GetAllAsync(param);

            return HandlePagedResult(topics);
        }
        
        [HttpGet("category/{id}")]
        public async Task<ActionResult<List<TopicDto>>> GetAllTopics(Guid id,[FromQuery] PagingParams param)
        {
            var topics = await ServiceManager.TopicService.GetAllByCategoryIdAsync(id,param);

            return HandlePagedResult(topics);
        }
        
        
        
        [HttpGet("{id}")]
        public async Task<ActionResult<List<TopicDto>>> GetTopicById(Guid id)
        {
            var topic = await ServiceManager.TopicService.GetByIdAsync(id);

            return Ok(topic);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic(TopicDto topic)
        {
            await ServiceManager.TopicService.CreateAsync(topic);

            return Ok();
        }
        [Authorize(Policy = "IsCreatorOrAdmin")]
        [HttpPut("{topicId}")]
        public async Task<IActionResult> EditTopic(Guid topicId, TopicDto topicDto)
        {
            await ServiceManager.TopicService.UpdateAsync(topicDto);
            return Ok();
        }
        [Authorize(Policy = "IsCreatorOrAdmin")]
        [HttpDelete("{topicId}")]
        public async Task<IActionResult> DeleteTopic(Guid topicId)
        {
            await ServiceManager.TopicService.DeleteAsync(topicId);
            return Ok();
        }

        [HttpPost("{topicId}/like")]
        public async Task<IActionResult> Like(Guid topicId)
        {
            await ServiceManager.LikeService.ToggleLikeAsync(topicId);

            return Ok();
        }

       
    }
}