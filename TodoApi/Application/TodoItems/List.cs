using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Application.TodoItems
{
    public class List
    {
        public class Query : IRequest<List<TodoItemDTO>>
        {
        }

        public class Handler : IRequestHandler<Query, List<TodoItemDTO>>
        {
            private readonly TodoContext _context;

            public Handler(TodoContext context)
            {
                _context = context;
            }

            public async Task<List<TodoItemDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.TodoItems.Select(x => x.ItemToDTO()).ToListAsync();
            }
        }
    }
}