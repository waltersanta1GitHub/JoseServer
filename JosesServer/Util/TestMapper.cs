using AutoMapper;
using JosesServer.Controllers.Poco;
using JosesServer.Repository.Models;

namespace JosesServer.Util
{
    public static class TestMapper
    { 
        // Map Poco and Models
        public static Mapper InitializeAutomapper() {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TestPoco, TestModel>()
                .ForMember(dest => dest.TestId,ori => ori.MapFrom( x=> x.TestId) )
                .ReverseMap();
                cfg.CreateMap<Question, QuestionModel>()
                .ForMember(dest => dest.QuestionId, ori => ori.MapFrom(x => x.QuestionId))

                .ReverseMap();
                cfg.CreateMap<TestResultsPoco, TestResultModel>()
                .ForMember(dest => dest.TestResultId, opt => opt.MapFrom(src => src.Id));

            });
           
            var mapper = new Mapper(config);
            return mapper;

        }
    }
}
