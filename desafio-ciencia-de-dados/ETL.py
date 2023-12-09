import pandas as pd
import requests
import json

sdw2023_api_url = 'https://sdw-2023-prd.up.railway.app'
df = pd.read_csv('SDW2023.csv')

user_ids = df['UserID'].tolist()
print(user_ids)

# Extração
def get_user(id):
    response = requests.get(f'{sdw2023_api_url}/users/{id}')
    return response.json() if response.status_code == 200 else None

users = [user for id in user_ids if (user := get_user(id)) is not None]
print(json.dumps(users, indent=2))

# Transformação/Enriquecimento

def generate_ai_news(user):
    completion_url = 'https://ron-swanson-quotes.herokuapp.com/v2/quotes'
    return requests.get(f'{completion_url}', json=user)

for user in users:
    news = generate_ai_news(user)
    print(news)

#Carregamento
def user_update(user):
    response = requests.put(f"{sdw2023_api_url}/users/{user['id']}", json=user)
    return True if response.status_code == 200 else False

for user in users:
    success = user_update(user)
    print(f"{user['name']} updated? \n {success}")