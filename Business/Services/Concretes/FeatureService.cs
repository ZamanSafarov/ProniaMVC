using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Data.RepositoryConcretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class FeatureService : IFeatureService
    {
        IFeatureRepository _featureRepository;

        public FeatureService(IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }
        public async Task AddFeature(Feature feature)
        {
            await _featureRepository.AddAsync(feature);
            await _featureRepository.CommitAsync();
        }

        public void DeleteFeature(int id)
        {
            var future = _featureRepository.Get(x => x.Id == id);
            _featureRepository.Delete(future);
           _featureRepository.Commit();
        }

        public List<Feature> GetAllFeatures(Func<Feature, bool>? func = null)
        {
            var future = _featureRepository.GetAll(x=>x.DeletedDate ==null);
            return future.ToList();
        }

        public Feature GetFeature(Func<Feature, bool>? func = null)
        {
            var future = _featureRepository.Get(x => x.DeletedDate == null);
            return future;
        }

        public void UpdateFeature(int id, Feature newFeature)
        {
            Feature oldFeature = _featureRepository.Get(x => x.Id == id);

            if (oldFeature == null) throw new NullReferenceException();

            oldFeature.Icon = newFeature.Icon;
            oldFeature.Title = newFeature.Title;
            oldFeature.Description = newFeature.Description;


            _featureRepository.Commit();
        }
    }
}
