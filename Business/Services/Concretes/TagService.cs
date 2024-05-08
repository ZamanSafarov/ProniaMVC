using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class TagService : ITagService
    {
        ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }


        public async Task AddTag(Tag tag)
        {
            if (!_tagRepository.GetAll().Any(x => x.Name == tag.Name))
            {

                await _tagRepository.AddAsync(tag);
                await _tagRepository.CommitAsync();
            }
        }

        public void DeleteTag(int id)
        {
            var existTag = _tagRepository.Get(x => x.Id == id);
            if (existTag == null) throw new NullReferenceException();

            _tagRepository.Delete(existTag);
            _tagRepository.Commit();

        }

        public List<Tag> GetAllTags(Func<Tag, bool>? func = null)
        {
            return _tagRepository.GetAll(func);
        }

        public Tag GetTag(Func<Tag, bool>? func = null)
        {
            return _tagRepository.Get(func);
        }

        public void UpdateTag(int id, Tag newTag)
        {
            Tag oldTag = _tagRepository.Get(x => x.Id == id);
            if (oldTag == null) throw new NullReferenceException();
            if (!_tagRepository.GetAll().Any(x => x.Name == newTag.Name))
            {
                oldTag.Name = newTag.Name;

            }
            _tagRepository.Commit();
        }
    }
}
