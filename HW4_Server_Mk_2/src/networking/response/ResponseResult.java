package networking.response;

import utility.GamePacket;

public class ResponseResult extends GameResponse {
    private int result;

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);

        packet.addInt32(result);

        return packet.getBytes();
    }

    public void setResult(int result){ this.result = result; }
}
