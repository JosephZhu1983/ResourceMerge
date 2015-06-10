using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceMerge.Core
{
    internal class ConfigSet
    {
        internal string Name = "Default";

        internal int ServerCacheDuration = 3600;

        internal int ClientCacheDuration = 3600;

        internal bool IsMergeStyle = true;

        internal bool IsMinifyStyle = true;

        internal bool IsMergeScript = true;

        internal bool IsMinifyScript = true;

        internal bool IsCompress = true;

        internal string Charset = "utf-8";

        internal bool GetCompressedRemoteResource = true;

        internal bool UseServerCache = true;

        internal bool UseClientCache = true;

        internal string StaticResrouceSuffix = string.Empty;

        internal string StaticResroucePreffix = string.Empty;

    }
}
