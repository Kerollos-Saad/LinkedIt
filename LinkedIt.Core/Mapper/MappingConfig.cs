﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.AppUsers;
using LinkedIt.Core.DTOs.Linker;
using LinkedIt.Core.DTOs.PhantomResignal;
using LinkedIt.Core.DTOs.PhantomSignal;
using LinkedIt.Core.DTOs.SignalComment;
using LinkedIt.Core.DTOs.SignalReaction;
using LinkedIt.Core.DTOs.User;
using LinkedIt.Core.DTOs.Whisper;
using LinkedIt.Core.DTOs.WhisperTalk;
using LinkedIt.Core.Models.Phantom_Signal;
using LinkedIt.Core.Models.User;
using LinkedIt.Core.Models.Whisper;

namespace LinkedIt.Core.Mapper
{
	public class MappingConfig : AutoMapper.Profile
	{
		public MappingConfig()
		{
			CreateMap<ApplicationUser, UserDTO>().ReverseMap(); // BothWays
			CreateMap<ApplicationUser, ApplicationUserToAddUserDTO>().ReverseMap(); // BothWays
			CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap(); // BothWays
			CreateMap<ApplicationUser, UpdateUserDTO>()
				.ReverseMap()
				.ForMember(dest => dest.BirthDate, opt => opt.Ignore())
				.ForAllMembers(opts => opts.Condition(
					(src, dest, srcMember) => srcMember != null));

			CreateMap<ApplicationUser, LinkerDTO>()
				.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>src.UserName))
				.ReverseMap();

			CreateMap<PhantomSignal, AddPhantomSignalDTO>().ReverseMap(); // BothWays
			CreateMap<PhantomSignal, PhantomSignalDTO>().ReverseMap(); // BothWays
			CreateMap<PhantomSignalComment, SignalCommentDetailsDTO>().ReverseMap(); // BothWays

			// Get phantom Signal With All details 
			// <PhantomSignal, PhantomSignalDTO> & <ApplicationUser, ApplicationUserDTO> Already Mapped
			CreateMap<PhantomSignalUp, SignalUpDTO>()
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
				.ForMember(dest => dest.SignalUpDate, opt => opt.MapFrom(src => src.SignalUpDate));

			CreateMap<PhantomSignalDown, SignalDownDTO>()
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
				.ForMember(dest => dest.SignalDownDate, opt => opt.MapFrom(src => src.SignalDownDate));

			CreateMap<PhantomSignalComment, SignalCommentDTO>()
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
				.ForMember(dest=>dest.Comment, opt => opt.MapFrom(src=> src.Comment))
				.ForMember(dest => dest.SignalCommentDate, opt => opt.MapFrom(src => src.SignalCommentDate));

			CreateMap<PhantomResignal, ResignalDTO>()
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
				.ForMember(dest => dest.ResignalDate, opt => opt.MapFrom(src => src.ResignalDate))
				.ForMember(dest => dest.ReSignalContent, opt => opt.MapFrom(src => src.ReSignalContent));

			CreateMap<PhantomSignal, PhantomSignalDetailsDTO>()
				.ForMember(dest => dest.PhantomSignal, opt => opt.MapFrom(src => src))
				.ForMember(dest => dest.SignalUser, opt => opt.MapFrom(src => src.ApplicationUser))
				.ForMember(dest => dest.SignalUps, opt => opt.MapFrom(src => src.PhantomSignalUps))
				.ForMember(dest => dest.SignalDowns, opt => opt.MapFrom(src => src.PhantomSignalDowns))
				.ForMember(dest => dest.SignalComments, opt => opt.MapFrom(src => src.PhantomSignalComments))
				.ForMember(dest => dest.Resignals, opt => opt.MapFrom(src => src.PhantomResignals));

			// Get Phantom ReSignal With All Details
			CreateMap<PhantomResignal, ResignalDetailsDTO>()
				.ForMember(dest => dest.Resignal, opt => opt.MapFrom(src => src))
				.ForMember(dest => dest.PhantomSignal, opt => opt.MapFrom(src => src.PhantomSignal))
				.ForMember(dest => dest.UserSignal, opt => opt.MapFrom(src => src.PhantomSignal.ApplicationUser))
				.ForMember(dest => dest.UserResignal, opt => opt.MapFrom(src => src.ApplicationUser));

			// Get Whisper With All Details

			CreateMap<WhisperTalk, WhisperTalkDetailsDTO>()
				.ForMember(dest => dest.SenderUserName, opt => opt.MapFrom(src => src.Sender.UserName));

			CreateMap<Whisper, WhisperDetailsDTO>();
		}
	}
}
