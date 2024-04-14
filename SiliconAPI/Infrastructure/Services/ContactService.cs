using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Net;

namespace Infrastructure.Services;

public class ContactService(ServiceService serviceService, ContactRepository contactRepository)
{
    private readonly ContactRepository _contactRepository = contactRepository;
    private readonly ServiceService _serviceService = serviceService;

    public async Task<HttpStatusCode> AddContact(ContactModel contact)
    {
        try
        {
            if (!await _serviceService.IsValidServiceAsync(contact.Service))
                return HttpStatusCode.BadRequest;

            var entity = new ContactEntity();
            entity.FullName = contact.FullName;
            entity.Email = contact.Email;
            entity.Message = contact.Message;

            var service = await _serviceService.GetServiceAsync(x => x.ServiceName == contact.Service);
            if (service == null)
                return HttpStatusCode.BadRequest;

            entity.ServiceId = service.Id;
            entity.Service = service;
            
            return await _contactRepository.CreateAsync(entity) != null ? HttpStatusCode.Created : HttpStatusCode.InternalServerError;
        }
        catch(Exception ex) { Debug.WriteLine(ex.Message); }

        return HttpStatusCode.InternalServerError;
    }
}
