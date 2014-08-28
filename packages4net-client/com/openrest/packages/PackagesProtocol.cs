﻿using System;
using Newtonsoft.Json;

namespace com.openrest.packages
{
    class PackagesProtocol
    {
        private readonly RestJsonClient restJsonClient;

        public PackagesProtocol()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new RequestJsonConvertor());

            restJsonClient = new RestJsonClient(settings);
        }

        public T Post<T>(Uri uri, Object obj)
        {
            try
            {
                Response<T> response = restJsonClient.Post<Response<T>>(uri, obj);
                VerifyResponse(response);
                return response.value;
            }
            catch (RestJsonHttpException e)
            {
                throw e;
            }
        }

        public RestJsonClient RestJsonClient
        {
            get
            {
                return restJsonClient;
            }
        }

        private static void VerifyResponse<T>(Response<T> response)
        {
            if (response.error != null)
            {
                throw new PackagesException(response.error);
            }
        }
    }
}
