using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.Planners;
using Microsoft.SemanticKernel.Planning;
using SK_Chat.Options;

namespace SK_Chat.Services;

public interface IGenerateService
{
    Task<string> Generate(string message);
}

internal sealed class GenerateService : IGenerateService
{
    private readonly IKernel _Kernel;

    public GenerateService(IOptions<LLMOption> options)
    {
        var builder = new KernelBuilder();

        if (options.Value.Type == "AzureOpenAI")
        {
            _Kernel = builder.WithAzureOpenAIChatCompletionService(
                deploymentName: options.Value.DeploymentName,
                endpoint: options.Value.Endpoint,
                apiKey: options.Value.Key!
            ).Build();
        }
        else if (options.Value.Type == "OpenAI")
        {
            _Kernel = builder.WithOpenAIChatCompletionService(
                modelId: options.Value.ModelId!,
                apiKey: options.Value.Key!
            ).Build();
        }
    }

    public async Task<string> Generate(string message)
    {
        var plan = await GetPlan(message);
        var variables = new ContextVariables
        {

        };
        var result = await plan.InvokeAsync(_Kernel, variables);
        return result.GetValue<string>()!.Trim();
    }

    public async Task<Plan> GetPlan(string goal)
    {
        GetPlugins();

        var planner = new SequentialPlanner(_Kernel);
        var plan = await planner.CreatePlanAsync(goal);
        return plan;
    }

    private void GetPlugins()
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
        _Kernel.ImportSemanticFunctionsFromDirectory(path, ["Starter"]);
        _Kernel.ImportFunctions(new Plugins.Math.Math());
    }
}
