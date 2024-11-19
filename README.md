# Blackbird.io xAI

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction
<!-- begin docs -->
The xAI application allows users to integrate AI-driven completion and chat capabilities into their workflows. Whether you're automating repetitive tasks or generating insights, this app makes advanced AI tools accessible.

## Before Setting Up

Before you can connect and start using xAI, ensure the following:
1. You have an active [xAI account](https://x.ai/) and API key. Visit xAI API Documentation for more details.
2. You are logged into your Blackbird.io environment.

## Connecting

1. Navigate to apps and search for xAI. If you cannot find xAI then click Add App in the top right corner, select xAI and add the app to your Blackbird environment.
2. Click Add Connection to set up the integration.
3. Name your connection for future reference e.g. 'My xAI connection'.
4. Enter your API key. You can generate one in your xAI account settings.
Format: `xai-XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX`.
5. Click Connect to complete the setup.

## Actions

**1. Create Completion**
Input Parameters:
- Model: Choose the model.
- Prompt: Text input to guide the model.
- Max Tokens: Limit the length of the response. (Optional)
- Temperature: Adjust randomness in output (0 = deterministic, 1 = creative). (Optional)
- Top P: Controls nucleus sampling. (Optional)
- Stop Sequences: Specify sequences to stop the completion. (Optional)
  
**Use Case:** Generate creative content, summarize text, or answer questions.

**2. Create Chat Completion**
Input Parameters:
- Model: Choose the desired model.
- Input: Provide a conversation messages.
- Max Tokens: Specify the response length. (Optional)
- Temperature: Adjust randomness in output (0 = deterministic, 1 = creative). (Optional)
- Top P: Controls nucleus sampling. (Optional)
- Stop Sequences: Specify sequences to stop the completion. (Optional)
- Presence Penalty: Higher values encourage the model to explore new topics and reduce repetition. (Optional)
- Frequency Penalty: Higher values discourage the model from repeating the same words or phrases multiple times in the same response. (Optional)
  
**Use Case:** Chatbots, virtual assistants, or conversational AI.

For more in-depth information about action consult the xAI [API reference](https://docs.x.ai/api).

## Feedback
Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
