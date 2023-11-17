// Copyright (c) Microsoft. All rights reserved.

namespace SK_Chat.Options;

public sealed class LLMOption
{
    public required string Type { get; set; } = "AzureOpenAI";
    public required string DeploymentName { get; set; }
    public required string Endpoint { get; set; }
    public string? Key { get; set; }
    public string? OrgId { get; set; } = "Personal";
    public string? ModelId { get; set; } = "gpt-3.5-turbo";
}
