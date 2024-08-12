from datetime import datetime
from ua_parser import user_agent_parser
from django.contrib.gis.geoip2 import GeoIP2

def get_visitor_info(request):
    # Get current date and time
    visit_datetime = datetime.now().strftime('%Y-%m-%d %H:%M:%S')

    # Get IP Address
    ip_address = request.META.get('HTTP_X_FORWARDED_FOR', request.META.get('REMOTE_ADDR'))

    # Get User Agent details
    user_agent_string = request.META.get('HTTP_USER_AGENT', '')
    parsed_user_agent = user_agent_parser.Parse(user_agent_string)
    
    # Basic browser and OS information
    browser_info = {
        'browser_family': parsed_user_agent['user_agent']['family'],
        'browser_version': f"{parsed_user_agent['user_agent']['major']}.{parsed_user_agent['user_agent']['minor']}",
        'os_family': parsed_user_agent['os']['family'],
        'os_version': f"{parsed_user_agent['os']['major']}.{parsed_user_agent['os']['minor']}",
        'device_family': parsed_user_agent['device']['family'],
    }

    # Get Host and Referrer
    host = request.META.get('HTTP_HOST', '')
    referrer = request.META.get('HTTP_REFERER', '')

    # Get Cookies
    cookies = request.COOKIES

    # Get Path and Query String
    path = request.path_info
    query_string = request.META.get('QUERY_STRING', '')

    # Get Session ID and Session Data
    session_id = request.session.session_key
    session_data = request.session.items()

    # Geolocation using GeoIP2
    g = GeoIP2()
    geo_info = {
        'country': None,
        'city': None,
        'latitude': None,
        'longitude': None
    }
    try:
        country = g.country(ip_address)
        city = g.city(ip_address)
        geo_info['country'] = country['country_name']
        geo_info['city'] = city['city']
        geo_info['latitude'] = city['latitude']
        geo_info['longitude'] = city['longitude']
    except Exception as e:
        geo_info['error'] = str(e)

    # Combine all information
    client_info = {
        'ip_address': ip_address,
        'visited_at': visit_datetime,
        'browser_info': browser_info,
        'host': host,
        'referrer': referrer,
        'cookies': cookies,
        'path': path,
        'query_string': query_string,
        'session_id': session_id,
        'session_data': dict(session_data),
        'geo_info': geo_info,
    }

    return client_info
