import time, json
from datetime import datetime
from django.core.mail import send_mail
from rest_framework.views import APIView
from rest_framework.response import Response
from rest_framework import status
from utils import chatgpt, visitors


class NewVisitorView(APIView):
    def post(self, request, *args, **kwargs):
        visitor_info = visitors.get_visitor_info(request)

        # Get the incoming JSON stringified data
        try:
            # Parse the incoming data (assuming it's in JSON format)
            incoming_data = json.loads(request.body.decode('utf-8'))
            visitor_info.update(incoming_data)
        except (json.JSONDecodeError, UnicodeDecodeError) as e:
            return Response({"error": "Invalid JSON format"}, status=status.HTTP_400_BAD_REQUEST)

        # Convert the client_info dictionary to a formatted JSON string
        visitor_info_json = json.dumps(visitor_info, indent=4)

        send_mail(
            f"A new visitor [{visitor_info['ip_address']}] to the staging server [{visitor_info['visited_url']}]",
            visitor_info_json,
            'mohamed.hana0@gmail.com',  # From email
            ['hana.pipeapps@gmail.com'],  # To emails
            fail_silently=False,
        )

        # data = chatgpt.get_completion("generate a chatgpt welcome message").content

        return Response(visitor_info, status=status.HTTP_200_OK)
