import time, json
from datetime import datetime
from django.core.mail import send_mail
from rest_framework.views import APIView
from rest_framework.response import Response
from rest_framework import status
from utils import chatgpt, visitors


class NewVisitorView(APIView):
    def get(self, request, *args, **kwargs):
        visitor_info = visitors.get_visitor_info(request)

        # Convert the client_info dictionary to a formatted JSON string
        visitor_info_json = json.dumps(visitor_info, indent=4)

        send_mail(
            f"A new visitor [{visitor_info['ip_address']}] to the staging server [https://d3jjitvwqr7c3v.cloudfront.net]",
            visitor_info_json,
            'mohamed.hana0@gmail.com',  # From email
            ['hana.pipeapps@gmail.com'],  # To emails
            fail_silently=False,
        )

        # data = chatgpt.get_completion("generate a chatgpt welcome message").content

        return Response(visitor_info, status=status.HTTP_200_OK)
