﻿using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Bookings.ReserveBooking
{
    internal sealed class BookingReservedCommandHandler(
        IUserRepository userRepository,
        IApartmentRepository apartmentRepository,
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork,
        PricingService pricingService,
        IDateTimeProvider dateTimeProvider) : ICommandHandler<ReserveBookingCommand, Guid>
    {
        private readonly IUserRepository _userRepository = userRepository;

        private readonly IApartmentRepository _apartmentRepository = apartmentRepository;
        private readonly IBookingRepository _bookingRepository=bookingRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly PricingService _pricingService = pricingService;
        private readonly IDateTimeProvider _dateTimeProvider=dateTimeProvider;
        public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId
                , cancellationToken);
            if (user == null) {
                return Result.Failure<Guid>(UserErrors.NotFound);
            }
            var apartment = await _apartmentRepository.GetByIdAsync(request.ApartmentId
                , cancellationToken);
            if (apartment == null)
            {
                return Result.Failure<Guid>(ApartmentErrors.NotFound);
            }
            var duration = DateRange.Create(request.StartDate, request.EndDate);
            if(await _bookingRepository.IsOverlappingAsync(apartment,duration,cancellationToken))
            {
                return Result.Failure<Guid>(BookingErrors.Overlap);
            }
            var booking = Booking.Reserve(
                apartment,
                user.Id,
                duration,
                _dateTimeProvider.UtcNow,
                _pricingService);
            _bookingRepository.Add(booking);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return booking.Id;

        }
    }
}
