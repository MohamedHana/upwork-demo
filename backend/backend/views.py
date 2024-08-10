
from rest_framework.views import APIView
from rest_framework.response import Response
from rest_framework import status

class TestAPIView(APIView):
    def get(self, request, *args, **kwargs):
        data = {
            "message": "Hello, this is a test endpoint!"
        }
        return Response(data, status=status.HTTP_200_OK)
