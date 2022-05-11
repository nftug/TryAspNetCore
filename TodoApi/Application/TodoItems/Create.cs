#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TodoApi.Models;

namespace TodoApi.Application.TodoItems
{
    public class Create
    {
        public class Command : IRequest
        {
            public TodoItem TodoItem { get; set; }
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
                request.TodoItem.CreatedAt = DateTime.Now;
                _context.TodoItems.Add(request.TodoItem);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}