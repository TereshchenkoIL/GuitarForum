using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Contracts.Paging;
using Contracts.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   
    public class TopicController : BaseApiController
    {

        public TopicController(IServiceManager serviceManager) : base(serviceManager)
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
        
        [HttpGet("creator/{id}")]
        public async Task<ActionResult<List<TopicDto>>> GetAllTopics(string id)
        {
            var topics = await ServiceManager.TopicService.GetAllByCreatorIdAsync(id);

            return Ok(topics);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic(TopicDto topic)
        {
            await ServiceManager.TopicService.CreateAsync(topic);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditTopic(TopicDto topicDto)
        {
            await ServiceManager.TopicService.CreateAsync(topicDto);
            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteTopic(TopicDto topicDto)
        {
            await ServiceManager.TopicService.DeleteAsync(topicDto);
            return Ok();
        }

        [HttpPost("{topicId}/like")]
        public async Task<IActionResult> Like(Guid topicId)
        {
            await ServiceManager.LikeService.CreateAsync(topicId);

            return Ok();
        }

       
    }
}