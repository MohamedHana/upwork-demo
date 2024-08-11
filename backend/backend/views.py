
from rest_framework.views import APIView
from rest_framework.response import Response
from rest_framework import status
from utils import chatgpt

class TestAPIView(APIView):
    def get(self, request, *args, **kwargs):
        data = chatgpt.get_completion("generate a chatgpt welcome message").content
        return Response(data, status=status.HTTP_200_OK)
