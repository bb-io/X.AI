# Blackbird.io xAI

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction
<!-- begin docs -->
The xAI application allows users to integrate AI-driven translation, editing, review, shortening, summarizing, chat and image generation into their workflows. Whether you're automating repetitive tasks or generating insights, this app makes advanced AI tools accessible.

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

### Translation

- **Translate** translates a file (XLIFF, HTML and other formats supported by Blackbird) into the target language. Only segments without a translation are sent to the model; locked units are skipped. Optional inputs: source language, additional instructions, glossary, output file handling, bucket size and document summary.
- **Translate text** translates a single text.

### Editing

- **Edit** post-edits the translated segments of a file and marks them as reviewed.
- **Edit text** post-edits a single translation. A target audience can be given in addition to the instructions.

### Review

- **Review** scores the translated and reviewed segments of a file between 0 and 1. Segments that score at or above the **Score threshold** (default 0.8) are set to final.
- **Review text** scores a single translation between 0 and 1.

### Shortening

- **Shorten content** shortens translations that are longer than their size restriction (`slr:sizeRestriction`) or the **Maximum length** input, retrying up to **Retries** times (default 3). Units that never fit are left unchanged.
- **Shorten text** shortens a text to the **Maximum length**. The action fails when the text never fits.

### Summarizing

- **Summarize** summarizes the content of a file, using the translation where there is one. Optional inputs: language, maximum words, target audience, additional instructions and glossary.
- **Summarize text** summarizes a single text.

### Chat completions

- **Chat completion** generates an answer to a message, with an optional system prompt, previous messages, max tokens, temperature, top P, stop sequences, presence penalty and frequency penalty. Reasoning models do not accept presence penalty, frequency penalty or stop sequences.

### Images

- **Generate image** generates an image from a prompt.

All translation, editing, review, shortening and summarizing actions accept an optional **Reasoning effort** (none, low, medium, high, extra high). Only some models support it, and not every model supports every value; the action fails before any processing when the chosen model does not support the value. When it is empty, the model's own default is used.

Problems that do not stop an action, such as segments the model could not process, are written to the log as warnings. File actions then finish and leave those segments unchanged; text actions fail with the warnings as the error message.

For more in-depth information about action consult the xAI [API reference](https://docs.x.ai/api).

## Feedback
Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
