using Business.Exceptions;
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
            if (!_featureRepository.GetAll().Any(x => x.Title == feature.Title))
            {
                await _featureRepository.AddAsync(feature);
                await _featureRepository.CommitAsync();
            }
            else
            {
                throw new DuplicateEntityException("Feature Already Has");
            }
            
        }

        public void DeleteFeature(int id)
        {
            var future = _featureRepository.Get(x => x.Id == id);
            if (future == null)
            {
                throw new EntityNotFoundException("Feature does not exsist");
            }
            _featureRepository.Delete(future);
            future.DeletedDate = DateTime.UtcNow.AddHours(4);
            _featureRepository.Commit();
        }

        public List<Feature> GetAllFeatures(Func<Feature, bool>? func = null)
        {
            var future = _featureRepository.GetAll(func);
            if (future == null)
            {
                throw new EntityNotFoundException("Features does not exsist");
            }
            return future.ToList();
        }

        public Feature GetFeature(Func<Feature, bool>? func = null)
        {
            var future = _featureRepository.Get(func);

            return future;
        }

        public void UpdateFeature(int id, Feature newFeature)
        {
            Feature oldFeature = _featureRepository.Get(x => x.Id == id);

            if (oldFeature == null) throw new EntityNotFoundException("Feature does not exsist");



            if (!_featureRepository.GetAll().Any(x => x.Title == newFeature.Title && x.Id != id))
            {
                oldFeature.Icon = newFeature.Icon;
                oldFeature.Title = newFeature.Title;
                oldFeature.Description = newFeature.Description;
            }
            else
            {
                throw new DuplicateEntityException("Feature Already Has");
            }

            _featureRepository.Commit();
        }
    }
}
