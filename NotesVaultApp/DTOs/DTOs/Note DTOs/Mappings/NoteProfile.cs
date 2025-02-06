using AutoMapper;
using NotesVaultApp.Data.Models;
using NotesVaultApp.Data.Models.Enums;

namespace NotesVaultApp.DTOs.Note_DTOs.Mappings
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<Note, NoteDto>()
                .ForMember(x => x.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore());

            CreateMap<CreateNoteDto, Note>()
                //.ForMember(dest => dest.Category, opt => opt.MapFrom(src => Enum.Parse<Categories>(src.Category.ToString())))
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<UpdateNoteDto, Note>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category { Name = Enum.Parse<Categories>(src.Category) }))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()); // From UpdateNoteDto to Note
        }
    }
}
