import os
from dotenv import load_dotenv, find_dotenv
from openai import OpenAI

# read local .env file
_ = load_dotenv(find_dotenv()) 

client = OpenAI(
    api_key=os.getenv("OPENAI_API_KEY"),
)

# (gpt-4o, gpt-4-turbo, gpt-3.5-turbo)
def get_completion(prompt, context="", model="gpt-4o", temperature=0, response_format=None):
    messages = []

    if context:
        messages.append({"role": "system", "content": context})

    messages.append({"role": "user", "content": prompt})

    completion = client.chat.completions.create(
        model=model,
        messages=messages,
        temperature=temperature,
        response_format=response_format,
    )
    
    return completion.choices[0].message

# (dall-e-2, dall-e-3)
def get_images(prompt="Generate random image", model="dall-e-3", size="1024x1024", n=1, quality="standard"):
    return client.images.generate(
        prompt=prompt,
        model=model,
        size=size,
        n=n,
        quality=quality,
    )
