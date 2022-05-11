using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.Exceptions;
using TodoApi.Models;

#nullable disable

namespace TodoApi.Application.TodoItems
{
    public class Edit
    {
        public class Command : IRequest
        {
            public TodoItem TodoItem { get; set; }
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly TodoContext _context;

            public Handler(TodoContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.Id != request.TodoItem.Id)
                {
                    throw new BadRequestException();
                }

                _context.Entry(request.TodoItem).State = EntityState.Modified;

                var item = await _context.TodoItems.FindAsync(request.Id);
                if (item == null)
                {
                    throw new NotFoundException();
                }
                request.TodoItem.CreatedAt = item.CreatedAt;

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}