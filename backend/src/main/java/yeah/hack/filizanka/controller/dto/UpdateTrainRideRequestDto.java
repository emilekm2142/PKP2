package yeah.hack.filizanka.controller.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class UpdateTrainRideRequestDto {

    private String trainRideId;

    private List<Long> pointIds;

    private Long trainId;
}
