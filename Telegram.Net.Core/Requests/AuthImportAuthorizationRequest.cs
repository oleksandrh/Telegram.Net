﻿using System;
using System.IO;
using Telegram.Net.Core.MTProto;

namespace Telegram.Net.Core.Requests
{
    public class AuthImportAuthorizationRequest: MTProtoRequest
    {
        private readonly int id;
        private readonly byte[] authKeyBytes;

        public AuthAuthorization authorization { get; private set; }

        public AuthImportAuthorizationRequest(int id, byte[] authKeyBytes)
        {
            this.id = id;
            this.authKeyBytes = authKeyBytes;
        }

        protected override uint requestCode => 0xe3ef9613;

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(id);
            Serializers.Bytes.Write(writer, authKeyBytes);
        }

        public override void OnResponse(BinaryReader reader)
        {
            authorization = TLObject.Read<AuthAuthorization>(reader);
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => true;
        public override bool Responded { get; }
    }
}