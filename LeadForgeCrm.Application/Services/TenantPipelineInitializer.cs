using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Entities.SaasCore;
using LeadForgeCrm.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace LeadForgeCrm.Application.Services
{
    public class TenantPipelineInitializer : ITenantPipelineInitializer
    {
        private readonly IPipelineTemplateRepository _pipelineTemplateRepo;
        private readonly IPipelineRepository _pipelineRepository;
        private readonly IPipelineStageRepository _pipelineStageRepository;
        private readonly ILogger<TenantPipelineInitializer> _logger;
        public TenantPipelineInitializer(IPipelineRepository pipelineRepository, IPipelineTemplateRepository pipelineTemplateRepository,ILogger<TenantPipelineInitializer> logger, IPipelineStageRepository pipelineStage)
        {
            _pipelineRepository = pipelineRepository;
            _pipelineTemplateRepo = pipelineTemplateRepository;
            _pipelineStageRepository = pipelineStage;
            _logger = logger;   

        }

        public async Task CreateDefaultPipelineAsync(Tenant tenant, CancellationToken ct)
        {
            var template = await _pipelineTemplateRepo.GetDefaultAsync();

            //later change
            if (template != null)
            {
                var pipeline = new PipeLine
                {
                    Tenant = tenant,
                    Name = template.Name,
                    IsDefault = true,
                    CreatedAt = DateTime.UtcNow
                };
                tenant.Pipelines.Add(pipeline);

                foreach (var stageTemplate in template.Stages.OrderBy(s => s.Order))
                {
                    pipeline.Stages.Add(new PipelineStage
                    {
                        Tenant = tenant,
                        Pipeline = pipeline,
                        Name = stageTemplate.Name,
                        Order = stageTemplate.Order,
                        DeafultProbability = stageTemplate.DefaultProbability,
                        CreatedAt = DateTime.UtcNow
                    });
                }

                await _pipelineRepository.AddAsync(pipeline);
            }
            else
                throw new Exception("Default pipeline template not found");
        }

        //public async Task UpdateAsync()
        //{
        //    //get pipeline of the user
        //    //list of pipeline stages contains in pipelineStages

        //    var pipelineStages =  await _pipelineStageRepository.GetUserPipeline();
            
        //    if(pipelineStages == null)
        //        throw new Exception("Pipeline not found");

        //    //order - 1 - from lead - converted
        //    pipelineStages.ChangeOrder(1);

        //    await _pipelineStageRepository.UpdateStatus(pipelineStages);
        //}
    }
}
