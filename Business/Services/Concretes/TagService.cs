using Business.Exceptions;
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
            else
            {
                throw new DuplicateEntityException("Tag Already Has");
            }
        }

        public void DeleteTag(int id)
        {
            var existTag = _tagRepository.Get(x => x.Id == id);
            if (existTag == null) throw new EntityNotFoundException("Tag does not exsist");

            _tagRepository.Delete(existTag);
            existTag.DeletedDate = DateTime.UtcNow.AddHours(4);
            _tagRepository.Commit();

        }

        public List<Tag> GetAllTags(Func<Tag, bool>? func = null)
        {
            var tags = _tagRepository.GetAll(func);

            if (tags is null) throw new EntityNotFoundException("Tag does not exsist");

            return tags;
        }

        public Tag GetTag(Func<Tag, bool>? func = null)
        {
            var tag = _tagRepository.Get(func);

            if (tag is null) throw new EntityNotFoundException("Tag does not exsist");

            return tag;
        }

        public void UpdateTag(int id, Tag newTag)
        {
            Tag oldTag = _tagRepository.Get(x => x.Id == id);
            if (oldTag == null) throw new EntityNotFoundException("Tag does not exsist");
            if (!_tagRepository.GetAll().Any(x => x.Name == newTag.Name && x.Id != id))
            {
                oldTag.Name = newTag.Name;

            }
            else
            {
                throw new DuplicateEntityException("Tag Already Has");
            }
            _tagRepository.Commit();
        }
    }
}
