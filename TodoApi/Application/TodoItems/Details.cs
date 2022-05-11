using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TodoApi.Models;
using System.Data;
using TodoApi.Exceptions;

namespace TodoApi.Application.TodoItems
{
    public class Details
    {
        public class Query : IRequest<TodoItemDTO>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, TodoItemDTO>
        {
            private readonly TodoContext _context;

            public Handler(TodoContext context)
            {
                _context = context;
            }

            public async Task<TodoItemDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var todoItem = await _context.TodoItems.FindAsync(request.Id);

                if (todoItem == null)
                {
                    throw new NotFoundException();
                }

                return todoItem.ItemToDTO();
            }
        }
    }
}