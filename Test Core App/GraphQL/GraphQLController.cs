using GrapeCity.DataService.Models;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GrapeCity.DataService.GraphQL
{
    [ApiVersionNeutral]
    [Route("northwind/graphql")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public GraphQLController(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            if (query == null) {
                return BadRequest();
            }
            using (var schema = new Schema { Query = new NorthwindQuery(_context) })
            {
                var inputs = query.Variables.ToInputs();
                var result = await new DocumentExecuter().ExecuteAsync(_ =>
                {
                    _.Inputs = inputs;
                    _.Schema = schema;
                    _.OperationName = query.OperationName;
                    _.Query = query.Query;

                }).ConfigureAwait(false);

                if (result.Errors?.Count > 0)
                {
                    return BadRequest();
                }

                return Ok(new { result.Data });  // TODO: return Ok(result) is not like .Net framework version, so only return the data part 
            }
        }
    }
}